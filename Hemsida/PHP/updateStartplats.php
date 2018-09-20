<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Startplats"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$OrderID=$_POST['OrderID']; 
$ID_Access=$_POST['ID_Access']; 
$ColumnName=$_POST['ColumnName']; 
$Value=$_POST['Value']; 

// Update
$sql= "UPDATE `$tbl_name` SET `$ColumnName`= '$Value' WHERE `OrderID` = '$OrderID' AND `ID_Access` = '$ID_Access' ";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
if($rc == 1)
{	
	echo "Updated row: OrderID: " . "'$OrderID'" . " ID_Access: " . "'$ID_Access'";	
}
else if ($rc == 0)
{
	echo "Nothing to update row: OrderID: " . "'$OrderID'" . " ID_Access: " . "'$ID_Access'";	
}
else if ($rc == -1)
{
	echo "Failure to update row " . "'$OrderID'" . " ID_Access: " . "'$ID_Access'" . "   " . "'$sql'";	
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>