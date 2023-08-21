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

    // Query to get Date, Time, and Reminder based on the username
    $reminderQuery = "SELECT id, date, time, reminder FROM reminders WHERE username = ? AND completed = 0";
    $reminderStmt = $conn->prepare($reminderQuery);
    $reminderStmt->bind_param("s", $Username);
    $reminderStmt->execute();
    $reminderStmt->bind_result($id, $date, $time, $reminder);

    $reminders = array();
    while ($reminderStmt->fetch()) {
        $reminders[] = array(
            "id" => $id,
            "date" => $date,
            "time" => $time,
            "reminder" => $reminder
        );
    }

    // Close the database connection
    $reminderStmt->close(); // Close this statement instead of $conn->close()

    // Output the reminders as JSON
    header('Content-Type: application/json');
    echo json_encode($reminders);
} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$stmt->close(); // Close the first statement here
?>
