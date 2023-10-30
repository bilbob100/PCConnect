<?php
$host = "HOST";
$username = "USERNAME";
$password = "PASSWORD";
$database = "DATABASE";

// Create a connection
$conn = new mysqli($host, $username, $password, $database);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Get the API key from the request header
$apiKey = $_SERVER['HTTP_X_API_KEY'];
$PCName = $_SERVER['HTTP_PCNAME'];

// Check if the hashed API key is valid
$validApiKeySql = "SELECT username FROM apikeys WHERE api_key = ?";
$validApiKeyStmt = $conn->prepare($validApiKeySql);
$validApiKeyStmt->bind_param("s", $apiKey);
$validApiKeyStmt->execute();
$validApiKeyResult = $validApiKeyStmt->get_result();
if ($validApiKeyResult->num_rows === 1) {
    $row = $validApiKeyResult->fetch_assoc();
    $Username = $row["username"]; // Get the valid username
    // API key is valid, proceed with the update
    // Assuming you have a variable $requestId containing the ID value
    // Prepare and execute the update statement
    $sql = "UPDATE requests SET Value = 0, Request = 0 WHERE Username = ? AND PCName = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("ss", $Username, $PCName);

    if ($stmt->execute()) {
        echo "Update successful";
    } else {
        echo "Update failed: " . $stmt->error;
    }

    $stmt->close();
} else {
    // Invalid API key
    echo "Invalid API key.";
}

$validApiKeyStmt->close();
$conn->close();
?>
