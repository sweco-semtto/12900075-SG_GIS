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

// ID and Text sent from form .NET
$ID=$_POST['ID']; 
$Text=$_POST['Text']; 
$ColumnName = $_POST['ColumnName'];

// Update
//$sql= "UPDATE `$tbl_name` SET Text = '$Text' WHERE ID = '$ID'";
$sql= "UPDATE `$tbl_name` SET `$ColumnName` = '$Text' WHERE ID = '$ID'";
$result = $con->query($sql);
$rc = $con->affected_rows;

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "<MessageXML><Data>";
if($rc == 1)
{	
	echo "Updated row. ";	
}
else
{
	echo "Failure";
}
echo "</Data></MessageXML>";

$result->free();
$con->close();
?>