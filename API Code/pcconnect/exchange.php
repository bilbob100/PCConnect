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
    // The API key is valid
    $stmt->fetch(); // Fetch the result into $dbUsername
    $Username = $dbUsername; // Assign the value to the $Username variable

    // Retrieve data from the request
    $Request = $_POST['Request'];
    $PCName = $_SERVER['HTTP_PCNAME'];

    // Use placeholders in SQL query and bind parameters
    $sql = "UPDATE requests SET Value = 1, Request = ? WHERE Username = ? AND PCName = ?";
    $updateStmt = $conn->prepare($sql);
    $updateStmt->bind_param("sss", $Request, $Username, $PCName);

    if ($updateStmt->execute()) {
        echo "Success";
    } else {
        echo "Error updating record: " . $updateStmt->error;
    }

    $updateStmt->close();
} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>
