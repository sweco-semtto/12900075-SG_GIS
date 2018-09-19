<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$ID=$_POST['ID']; 
$Text=$_POST['Text']; 
$ColumnName = $_POST['ColumnName'];

// Update
//$sql= "UPDATE `$tbl_name` SET Text = '$Text' WHERE ID = '$ID'";
$sql= "UPDATE `$tbl_name` SET `$ColumnName` = '$Text' WHERE ID = '$ID'";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

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

mysql_close();
?>