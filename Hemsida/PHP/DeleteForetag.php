<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Foretag"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// Data sent from form .NET
$OrderID=$_POST['OrderID'];

// Delete
$sql="DELETE FROM `$tbl_name` WHERE OrderID = '$OrderID'";
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
