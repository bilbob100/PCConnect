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
$stmt->bind_result($dbUsername); // Bind the result to a variable
$stmt->store_result();

if ($stmt->num_rows > 0) {
    $stmt->fetch(); // Fetch the result into $dbUsername
    $Username = $dbUsername; // Assign the value to the $Username variable
    // Get the Date, Time, and Reminder from POST request
    $PCName = $_SERVER['HTTP_PCNAME'];


    if ($PCName == ""){

        echo("Please fill in all the fields");
        exit();
    }

    // Insert data into the reminders table
    $insertQuery = "INSERT INTO pcnames (Username, PCName) VALUES (?, ?)";
    $insertStmt = $conn->prepare($insertQuery);
    $insertStmt->bind_param("ss", $Username, $PCName);

    if ($insertStmt->execute()) {
        $insertRequest = "INSERT INTO requests (Username, PCName, Request, Value) VALUES (?, ?, ?, ?)";
        $insertRequestStmt = $conn->prepare($insertRequest);
        $DefaultValue=0;
        $insertRequestStmt->bind_param("sssi", $Username, $PCName, $DefaultValue, $DefaultValue);
        if ($insertRequestStmt->execute()) {
            $insertTime= "INSERT INTO time (Username, PCName) VALUES (?, ?)";
            $insertTimeStmt = $conn->prepare($insertTime);
            $insertTimeStmt->bind_param("ss", $Username, $PCName);
            if ($insertTimeStmt->execute()) {
                echo("PC added successfully");

            } else {
                echo("Error adding PC: " . $insertRequestStmt->error);
            }

        } else {
            echo("Error adding PC: " . $insertRequestStmt->error);
        }
    } else {
        echo("Error adding PC: " . $insertStmt->error);
    }

} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>

