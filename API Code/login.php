<?php
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

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $inputUsername = $_POST["loginUsername"];
    $inputPassword = hash("sha256", $_POST["loginPassword"]); // Assuming password is already SHA-256 encrypted

    // Query to check if the provided username and password exist in the users table
    $query = "SELECT id FROM users WHERE username = ? AND password = ?";
    $stmt = $conn->prepare($query);
    $stmt->bind_param("ss", $inputUsername, $inputPassword);
    $stmt->execute();
    $stmt->store_result();

    if ($stmt->num_rows > 0) {
        // Retrieve the existing API key for the user from the apikeys table
        $apiKeyQuery = "SELECT api_key FROM apikeys WHERE username = ?";
        $apiKeyStmt = $conn->prepare($apiKeyQuery);
        $apiKeyStmt->bind_param("s", $inputUsername);
        $apiKeyStmt->execute();
        $apiKeyStmt->bind_result($existingApiKey);
        $apiKeyStmt->fetch();

        echo $existingApiKey; // Output the API key for successful login
    } else {
        echo "Invalid username or password."; // Output for failed login
    }
}
?>
