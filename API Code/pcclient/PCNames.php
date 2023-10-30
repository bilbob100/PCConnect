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

    // Use placeholders in SQL query and bind parameters
    $query = "SELECT PCName FROM pcnames WHERE Username = ?";
    $pcnameStmt = $conn->prepare($query);
    $pcnameStmt->bind_param("s", $Username);
    $pcnameStmt->execute();

    $pcnameResult = $pcnameStmt->get_result();

    $pcNames = array(); // Create an array to store PCNames

    while ($row = $pcnameResult->fetch_assoc()) {
        $pcNames[] = $row['PCName']; // Add each PCName to the array
    }

    // Set the PCNames in the response
    header('Content-Type: application/json');
    echo json_encode(array("PCNames" => $pcNames));

    $pcnameStmt->close();
} else {
    // The provided API key is invalid
    echo "Invalid API key";
}

// Close the database connection
$conn->close();
?>
