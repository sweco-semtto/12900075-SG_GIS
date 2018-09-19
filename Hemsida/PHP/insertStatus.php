<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Status"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$ID             = $_POST['ID'];
$StartplatsID	= $_POST['StartplatsID'];
$Datum          = $_POST['Datum']; 
$AndradAv       = $_POST['AndradAv']; 
$Status_status  = $_POST['Status_status'];

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES ('$ID', '$StartplatsID', '$Datum' , '$AndradAv', '$Status_status')";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

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

mysql_close();
?>
