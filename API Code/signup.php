<?php
$host = "HOST";
$username = "USERNAME";
$password = "PASSWORD";
$database = "DATABASE";

$conn = new mysqli($host, $username, $password, $database);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $inputName = $_POST["name"];
    $inputUsername = $_POST["username"];
    $inputEmail = $_POST["email"];
    $inputDateOfBirth = $_POST["dob"]; // Date of birth

    // Check if email already exists in the users table
    $emailCheckQuery = "SELECT * FROM users WHERE Email = ?";
    $emailCheckStmt = $conn->prepare($emailCheckQuery);
    $emailCheckStmt->bind_param("s", $inputEmail);
    $emailCheckStmt->execute();
    $emailCheckResult = $emailCheckStmt->get_result();

    if ($emailCheckResult && $emailCheckResult->num_rows > 0) {
        $errorMessage = "Email already exists, please login.";
    } else {
        // Check if username already exists in the users table
        $usernameCheckQuery = "SELECT * FROM users WHERE Username = ?";
        $usernameCheckStmt = $conn->prepare($usernameCheckQuery);
        $usernameCheckStmt->bind_param("s", $inputUsername);
        $usernameCheckStmt->execute();
        $usernameCheckResult = $usernameCheckStmt->get_result();

        if ($usernameCheckResult && $usernameCheckResult->num_rows > 0) {
            $errorMessage = "Username already exists, please choose a different one.";
        } else {
            $inputPassword = hash("sha256", $_POST["password"]);

            // Insert user into users table
            $insertUserQuery = "INSERT INTO users (Name, Username, DateOfBirth, Email, Password) VALUES (?, ?, ?, ?, ?)";
            $insertUserStmt = $conn->prepare($insertUserQuery);
            $insertUserStmt->bind_param("sssss",  $inputName, $inputUsername, $inputDateOfBirth, $inputEmail, $inputPassword);
            $insertUserStmt->execute();

            $insertRequestQuery = "INSERT INTO requests (Username, Request, Value) VALUES (?, ?, ?)";
            $insertRequestTemplate = $conn->prepare($insertRequestQuery);
            $insertRequestTemplate->bind_param("ssi", $inputUsername, $requestValue, $requestValue);
            $requestValue = 0; // Replace with the actual request value
            $insertRequestTemplate->execute();
            $insertRequestTemplate->close();
        
            $insertTimeQuery = "INSERT INTO time (Username) VALUES (?)";
            $insertTimeTemplate = $conn->prepare($insertTimeQuery);
            $insertTimeTemplate->bind_param("s", $inputUsername);
            $insertTimeTemplate->execute();
            $insertTimeTemplate->close();
        
            // Generate an API key for the newly signed up user
            $apiKey = generateApiKey();
        
            // Insert the API key into the apikeys table
            $insertApiKeyQuery = "INSERT INTO apikeys (username, api_key) VALUES (?, ?)";
            $insertApiKeyStmt = $conn->prepare($insertApiKeyQuery);
            $insertApiKeyStmt->bind_param("ss", $inputUsername, $apiKey);
            $insertApiKeyStmt->execute();
        
            $response = array("status" => "success");
            
            header("Location: https://pcconnect.adamkhattab.co.uk/sign-up-success.html");
            exit(); // Important: Stop further execution of the script
            
        }
    }
    
}

function generateApiKey() {
    return bin2hex(random_bytes(16)); // Generate a random API key
}

?>
<!DOCTYPE html>
<html>
<head>
    <title>Sign Up Result</title>
</head>
<body>
    <?php if (!empty($errorMessage)) { ?>
        <p style="color: red;"><?php echo $errorMessage; ?></p>
    <?php } ?>
</body>
</html>
