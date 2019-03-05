<?php 
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Ordernummer"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($mysqli->connect_errno) {
	printf("Anslutningsfel: %s\n", $mysqli->connect_error);
	exit();
}	
	
// Data sent from form javascript
$Year = $_POST['Year'];
$Timestamp = $_POST['Timestamp']; 

// Tar fram vilket som är högsta id-numret och lägger till ett. 
$sql= "SELECT Ordernummer FROM `$tbl_name` where Ar='" . $Year . "'";

$result = $con->query($sql);	
$row = $result->fetch_array(MYSQLI_ASSOC);

$Ordernummer = 0;
$Ordernummer = intval($row["Ordernummer"]);
while($row = $result->fetch_assoc())
{
	$Ordernummer = intval($row["Ordernummer"]);
}

// Ökar på med ett så vi får nästa ordernummer. 
$Ordernummer = $Ordernummer + 1;

// Skapar ett nytt ordernummer
$sql="INSERT INTO `$tbl_name` (Ordernummer, Ar, Tid) VALUES ($Ordernummer, '$Year', '$Timestamp')";	
$result = $con->query($sql);
$rc = $con->affected_rows;
	
// Kontrollerar att en rad skapades för att veta att ordernumret finns i databasen. 
if ($rc == 1)
{
	echo "" . $Ordernummer;
}
else
{
	echo "0";
}
?>