<?php
// Get the API key from the X-API-Key header
$apiKey = $_SERVER["HTTP_X_API_KEY"];

// Validate the API key by checking if it exists in the apikeys table
$host = "HOST";
$username = "USERNAME";
$password = "PASSWORD";
$database = "DATABASE";

// Establish a database connection
$conn = new mysqli($host, $username, $password, $database);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Query to check if the provided API key exists in the apikeys table
$query = "SELECT username FROM apikeys WHERE api_key = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param("s", $apiKey);
$stmt->execute();
$stmt->store_result();

if ($stmt->num_rows > 0) {
    // The API key is valid
    $url1 = 'https://pcconnect.adamkhattab.co.uk/api/time.php';

    $url2 = 'https://pcconnect.adamkhattab.co.uk/api/pcconnect/pctime.php';

    // Initialize cURL session for $url2
    $ch1 = curl_init($url1);
    $ch2 = curl_init($url2);
    
    curl_setopt($ch1, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch2, CURLOPT_RETURNTRANSFER, true);

    // Set the API key in the cURL header
    
    curl_setopt($ch2, CURLOPT_HTTPHEADER, array("X-API-Key: $apiKey"));

    // Execute cURL request
    $response1 = curl_exec($ch1);
    $response2 = curl_exec($ch2);

    curl_close($ch1);
    curl_close($ch2);

    // Parse the JSON response from $url2
    $data1 = json_decode($response1, true);
    $data2 = json_decode($response2, true);

    if (isset($data2['time'])) {
        $time1 = $data1['time'];
        $time2 = $data2['time'];

        $query = "SELECT Time FROM time WHERE ID = 1";
        $result = $conn->query($query);


            // Convert times to Unix timestamps
            $timestamp1 = strtotime($time1);
            $timestamp2 = strtotime($time2);

            // Define the allowed time difference in seconds
            $errorInterval = 2;

            // Compare the timestamps within the error interval
            if (abs($timestamp1 - $timestamp2) <= $errorInterval) {
                echo "yes";
            } else {
                echo "no";
            }

    } else {
        echo "Error fetching data from $url2";
    }
} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>
