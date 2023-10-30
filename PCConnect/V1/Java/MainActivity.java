package com.adamkhattab.pcconnect;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.content.Context;
import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class MainActivity extends AppCompatActivity {

    private static final String TAG = "MainActivity";
    private TextView resultTextView;
    private Button sleepButton;
    private Button hibernateButton;
    private Button shutdownButton;
    private Button lockButton;
    private Button signoutButton;
    private Button logOutAppButton;
    private Button reminderButton;
    private Button viewReminderButton;
    private Handler handler = new Handler();
    private Runnable checkInternetStatusRunnable = new Runnable() {
        @Override
        public void run() {
            new CheckInternetStatusTask(getApplicationContext()).execute();
            handler.postDelayed(this, 1000); // Check every 5 seconds
        }
    };

//    @SuppressLint("MissingInflatedId")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        resultTextView = findViewById(R.id.Result);
        sleepButton = findViewById(R.id.Sleep);
        hibernateButton = findViewById(R.id.Hibernate);
        shutdownButton = findViewById(R.id.Shutdown);
        lockButton = findViewById(R.id.Lock);
        signoutButton = findViewById(R.id.Signout);
        logOutAppButton = findViewById(R.id.LogOutAPP);
        reminderButton = findViewById(R.id.Reminder);
        viewReminderButton = findViewById(R.id.ViewReminders);
        String apiKey = SharedPrefManager.getInstance(this).getApiKey();


        sleepButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                NetworkUtils.Sleep(getApplicationContext());
                resultTextView.setText("PC has been placed on sleep");
                clearResultTextDelayed();

            }
        });

        hibernateButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                NetworkUtils.Hibernate(getApplicationContext()); // Or pass the appropriate context
                resultTextView.setText("PC has been placed on hibernate");
                clearResultTextDelayed();
            }
        });

        shutdownButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                NetworkUtils.Shutdown(getApplicationContext());
                resultTextView.setText("PC has been placed on shutdown");
                clearResultTextDelayed();
            }
        });

        lockButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                NetworkUtils.Lock(getApplicationContext());
                resultTextView.setText("PC has been locked");
                clearResultTextDelayed();
            }
        });

        signoutButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                NetworkUtils.Signout(getApplicationContext());
                resultTextView.setText("PC has been signed out");
                clearResultTextDelayed();
            }
        });

        reminderButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                Intent intent = new Intent(MainActivity.this, ReminderActivity.class);
                startActivity(intent);

            }
        });

        viewReminderButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {
                new MyAsyncTask().execute();
                Intent intent = new Intent(MainActivity.this, ListRemindersActivity.class);
                startActivity(intent);

            }
        });

        logOutAppButton.setOnClickListener(new View.OnClickListener() {
            @SuppressLint("SetTextI18n")
            @Override
            public void onClick(View v) {

                SharedPrefManager.getInstance(MainActivity.this).clearApiKey();
                Intent intent = new Intent(MainActivity.this, LoginActivity.class);
                startActivity(intent);
                finish(); // Optional: Close the MainActivity to prevent going back to it

            }
        });

        // Call the network request on startup
        new MyAsyncTask().execute();

        handler.postDelayed(checkInternetStatusRunnable, 0); // Start checking immediately
    }

    private void clearResultTextDelayed() {
        Handler handler = new Handler();
        handler.postDelayed(new Runnable() {
            @Override
            public void run() {
                resultTextView.setText("");
            }
        }, 5000); // 2000 milliseconds (2 seconds)
    }

    private class MyAsyncTask extends AsyncTask<Void, Void, String> {

        @Override
        protected String doInBackground(Void... voids) {
            StringBuilder result = new StringBuilder();

            try {
                URL urlObject = new URL("PHP_URL/pcconnect/checkinternet.php");
                HttpURLConnection connection = (HttpURLConnection) urlObject.openConnection();

                if (connection.getResponseCode() == HttpURLConnection.HTTP_OK) {
                    InputStream inputStream = connection.getInputStream();
                    BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));
                    String line;

                    while ((line = reader.readLine()) != null) {
                        result.append(line);
                    }

                    reader.close();
                } else {
                    Log.e(TAG, "HTTP Request Error: " + connection.getResponseCode());
                }
            } catch (IOException e) {
                Log.e(TAG, "Error making HTTP request: " + e.getMessage());
            }

            return result.toString();
        }

        @Override
        protected void onPostExecute(String result) {
            super.onPostExecute(result);
            // Here you can handle the extracted output as needed
            Log.d(TAG, "Received output: " + result);
//            resultTextView.setText(result);
        }
    }

    private class CheckInternetStatusTask extends AsyncTask<Void, Void, String> {

        private Context context;

        public CheckInternetStatusTask(Context context) {
            this.context = context;
        }

        @Override
        protected String doInBackground(Void... voids) {
            StringBuilder result = new StringBuilder();

            try {
                URL urlObject = new URL("PHP_URL/pcconnect/checkinternet.php");
                HttpURLConnection connection = (HttpURLConnection) urlObject.openConnection();

                // Get the API key from shared preferences
                String apiKey = SharedPrefManager.getInstance(context).getApiKey();

                // Set the API key in the request header
                connection.setRequestProperty("X-API-Key", apiKey);

                if (connection.getResponseCode() == HttpURLConnection.HTTP_OK) {
                    InputStream inputStream = connection.getInputStream();
                    BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));
                    String line;

                    while ((line = reader.readLine()) != null) {
                        result.append(line);
                    }

                    reader.close();
                } else {
                    Log.e(TAG, "HTTP Request Error: " + connection.getResponseCode());
                }
            } catch (IOException e) {
                Log.e(TAG, "Error making HTTP request: " + e.getMessage());
            }

            return result.toString();
        }

        @Override
        protected void onPostExecute(String result) {
            super.onPostExecute(result);

            if ("yes".equalsIgnoreCase(result)) {
                sleepButton.setEnabled(true);
                hibernateButton.setEnabled(true);
                shutdownButton.setEnabled(true);
                lockButton.setEnabled(true);
                signoutButton.setEnabled(true);
            } else {
                sleepButton.setEnabled(false);
                hibernateButton.setEnabled(false);
                shutdownButton.setEnabled(false);
                lockButton.setEnabled(false);
                signoutButton.setEnabled(false);
            }
        }
    }


    @Override
    protected void onDestroy() {
        super.onDestroy();
        handler.removeCallbacks(checkInternetStatusRunnable);
    }
}
