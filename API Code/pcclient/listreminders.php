<?php

function decryptString($data, $apiKey) {
    $cipher = "aes-256-cbc";
    $data = base64_decode($data);
    $ivlen = openssl_cipher_iv_length($cipher);
    $iv = substr($data, 0, $ivlen);
    $data = substr($data, $ivlen);

    try {
        if (strlen($iv) !== $ivlen) {
            throw new Exception('IV length mismatch');
        }

        $decrypted = openssl_decrypt($data, $cipher, $apiKey, 0, $iv);

        if ($decrypted === false) {
            throw new Exception('Decryption failed');
        }

        return $decrypted;
    } catch (Exception $e) {
        return "Error decrypting";
    }
}



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
    // The API key is valid
    $stmt->fetch(); // Fetch the result into $dbUsername
    $Username = $dbUsername; // Assign the value to the $Username variable

    // Retrieve reminders for the provided username
    $getRemindersQuery = "SELECT ID, Username, Date, Time, Reminder, Completed FROM reminders WHERE username = ? ORDER BY STR_TO_DATE(CONCAT(Date, ' ', Time), '%d/%m/%Y %H:%i') DESC";

    $getRemindersStmt = $conn->prepare($getRemindersQuery);
    $getRemindersStmt->bind_param("s", $Username);
    $getRemindersStmt->execute();
    $result = $getRemindersStmt->get_result();
    // Create an array to hold the reminders
    $reminders = array();

    // Fetch reminders, decrypt the Reminder column, and add to the array
    while ($row = $result->fetch_assoc()) {
        $row['Reminder'] = decryptString($row['Reminder'], $apiKey);
        $reminders[] = $row;
    }

    // Close the statement
    $getRemindersStmt->close();

    // Return the reminders as JSON
    header("Content-Type: application/json");
    echo json_encode($reminders);
} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>
