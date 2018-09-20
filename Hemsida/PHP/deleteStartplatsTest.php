<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test_Startplats"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// OrderID and Text sent from form .NET
$OrderID=$_POST['OrderID']; 

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);

// Insert Into
//$sql="DELETE FROM `$tbl_name` WHERE OrderID = '$OrderID'"; //Delete sker genom att flagga som borttagen
$sql="UPDATE `$tbl_name` SET Borttagen = 1 WHERE OrderID = '$OrderID'";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";
if ($rc == 1)	
{
	echo "Success, deleted ID " . "'$OrderID'";
}
else
{
	echo "Failure, not deleted ID " . "'$OrderID'";
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>
