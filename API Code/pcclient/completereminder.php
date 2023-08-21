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

    // Check if POST data contains "id"
    if (isset($_POST['id'])) {
        $id = $_POST['id'];

        // Update the "completed" value to 1 for the given "id"
        $updateQuery = "UPDATE reminders SET completed = 1 WHERE id = ? AND username = ?";
        $updateStmt = $conn->prepare($updateQuery);
        $updateStmt->bind_param("is", $id, $Username);
        $updateResult = $updateStmt->execute();

        if ($updateResult) {
            echo "Record with ID $id has been marked as completed.";
        } else {
            echo "Failed to update the record.";
        }
    } else {
        echo "Missing 'id' parameter.";
    }

} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>
