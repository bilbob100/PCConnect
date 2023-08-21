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
$apiKey = isset($_SERVER['HTTP_X_API_KEY']) ? $_SERVER['HTTP_X_API_KEY'] : '';

// Check if the hashed API key is valid
$validApiKeySql = "SELECT username FROM apikeys WHERE api_key = ?";
$validApiKeyStmt = $conn->prepare($validApiKeySql);
$validApiKeyStmt->bind_param("s", $apiKey);
$validApiKeyStmt->execute();
$validApiKeyResult = $validApiKeyStmt->get_result();

if ($validApiKeyResult->num_rows === 1) {
    $row = $validApiKeyResult->fetch_assoc();
    $username = $row["username"]; // Get the valid username

    // Retrieve data from the request
    $timePython = isset($_POST['timePython']) ? $_POST['timePython'] : '';

    if (!empty($timePython)) {
        // Use prepared statement to update the value
        $sql = "UPDATE time SET Time = ? WHERE Username = ?";
        $stmt = $conn->prepare($sql);

        // Bind the parameters
        $stmt->bind_param("ss", $timePython, $username); // 's' represents a string, adjust if necessary

        if ($stmt->execute()) {
            echo "Success";
        } else {
            echo "Error updating record: " . $stmt->error;
        }

        $stmt->close();
    } else {
        echo "Error: timePython parameter is missing or empty.";
    }
} else {
    // Invalid API key
    echo "Invalid API key.";
}

$validApiKeyStmt->close();
$conn->close();
?>
