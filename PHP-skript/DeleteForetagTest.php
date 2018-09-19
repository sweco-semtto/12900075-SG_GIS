<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Test_Foretag"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// Data sent from form .NET
$OrderID=$_POST['OrderID'];

// Delete
//$sql="DELETE FROM `$tbl_name` WHERE OrderID = '$OrderID'"; //Delete sker genom att flagga som borttagen
$sql="UPDATE `$tbl_name` SET Borttagen = 1 WHERE OrderID = '$OrderID'";
$result=mysql_query($sql);
$affectedRows = mysql_affected_rows();

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";
if ($affectedRows == 1)	
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
