<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test_Status"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($con->connect_errno) {
    printf("Anslutningsfel: %s\n", $con->connect_error);
    exit();
}

// ID and Text sent from form .NET
$ID             = $_POST['ID'];
$StartplatsID	= $_POST['StartplatsID'];
$Datum          = $_POST['Datum']; 
$AndradAv       = $_POST['AndradAv']; 
$Status_status  = $_POST['Status_status'];

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES ('$ID', '$StartplatsID', '$Datum' , '$AndradAv', '$Status_status', '0')";
$result = $con->query($sql);
$rc = $con->affected_rows;

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "\n";
echo "<Data>";
if ($rc == 1) // i.e. ($num_rows_after - $num_rows_before == 1)	
{
	echo "Success, inserted " . "'$ID'";
}
else
{
	echo "Failure, not inserted " . "'$ID'";
	echo "\n";
	echo $sql;
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

//$result->free();
//$con->close();
?>