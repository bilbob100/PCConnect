<?php
// Set the content type to JSON
header('Content-Type: application/json');

// Set the timezone to UTC (you can change this to your desired timezone)
date_default_timezone_set('Europe/London');

// Get the current time
$current_time = date('H:i:s');

// Create a response array
$response = array('time' => $current_time);

// Output the response as JSON
echo json_encode($response);
?>
