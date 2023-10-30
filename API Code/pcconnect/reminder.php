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

function encryptString($data, $apiKey) {
    $cipher = "aes-256-cbc";
    $ivlen = openssl_cipher_iv_length($cipher);
    $iv = openssl_random_pseudo_bytes($ivlen);
    $encrypted = openssl_encrypt($data, $cipher, $apiKey, 0, $iv);

    if ($encrypted === false) {
        die('Encryption failed');
    }

    return base64_encode($iv . $encrypted);
}

// Query to check if the provided API key exists in the apikeys table
$query = "SELECT username FROM apikeys WHERE api_key = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param("s", $apiKey);
$stmt->execute();
$stmt->bind_result($dbUsername); // Bind the result to a variable
$stmt->store_result();

if ($stmt->num_rows > 0) {
    $stmt->fetch(); // Fetch the result into $dbUsername
    $Username = $dbUsername; // Assign the value to the $Username variable

    // Get the Date, Time, and Reminder from POST request
    $date = $_POST["date"];
    $time = $_POST["time"];
    $reminder = $_POST["reminder"];

    if ($date == "" || $time == "" || $reminder == ""){

        echo("Please fill in all the fields");
        exit();
    }
    $reminder=encryptString($reminder, $apiKey);

    // Insert data into the reminders table
    $insertQuery = "INSERT INTO reminders (username, date, time, reminder) VALUES (?, ?, ?, ?)";
    $insertStmt = $conn->prepare($insertQuery);
    $insertStmt->bind_param("ssss", $Username, $date, $time, $reminder);

    if ($insertStmt->execute()) {
        echo "Reminder inserted successfully!";
    } else {
        echo "Error inserting reminder: " . $insertStmt->error;
    }

} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>

