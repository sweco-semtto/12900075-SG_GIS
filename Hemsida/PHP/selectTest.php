<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($mysqli->connect_errno) {
    printf("Anslutningsfel: %s\n", $mysqli->connect_error);
    exit();
}

// Select
$sql="SELECT * FROM `$tbl_name`";
$result = $con->query($sql);
$row = $result->fetch_array(MYSQLI_ASSOC);

// To .NET
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";

	$count = $result->num_rows;
	echo "<NoOfRows>";
	echo "\n";
	echo "Antal: " . "'$count'";
	echo "\n";
	echo "</NoOfRows>";
	echo "\n";

	while($row = $result->fetch_assoc())
	{
		echo "<Row>";
		echo "\n";
		echo "ID: " . $row["ID"] . "    Text: " . $row["Text"];
		echo "\n";
		echo "</Row>";
		echo "\n";
	}

echo "</Data>";
echo "\n";
echo "</MessageXML>";

$result->free();
$con->close();
?>
