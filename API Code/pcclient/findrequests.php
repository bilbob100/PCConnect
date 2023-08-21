<?php
// Database configuration
$host = "HOST";
$username = "USERNAME";
$password = "PASSWORD";
$database = "DATABASE";


// Create connection
$conn = new mysqli($host, $username, $password, $database);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Get the API key from the request header
$apiKey = $_SERVER['HTTP_X_API_KEY'];

// Check if the hashed API key is valid
$validApiKeySql = "SELECT username FROM apikeys WHERE api_key = ?";
$validApiKeyStmt = $conn->prepare($validApiKeySql);
$validApiKeyStmt->bind_param("s", $apiKey);
$validApiKeyStmt->execute();
$validApiKeyResult = $validApiKeyStmt->get_result();

if ($validApiKeyResult->num_rows === 1) {
    $row = $validApiKeyResult->fetch_assoc();
    $Username = $row["username"]; // Get the valid username

    // Proceed with the SQL statement using placeholders and prepared statements
    $sql = "SELECT Request FROM requests WHERE Value = '1' AND Username = ?";
    $stmt = $conn->prepare($sql);
    if ($stmt) {
        $stmt->bind_param("s", $Username);
        $stmt->execute();
        $result = $stmt->get_result();

        if ($result->num_rows > 0) {
            // Output data in the required format
            while ($row = $result->fetch_assoc()) {
                echo $row["Request"];
            }
        } else {
            echo "No requests found.";
        }

        $stmt->close();
    } else {
        echo "Failed to prepare the statement.";
    }
} else {
    // Invalid API key
    echo "Invalid API key.";
}

$conn->close();
?>
