<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$myID=$_POST['ID']; 

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);

// Select * Before
//$sql_select_all = "SELECT * FROM `$tbl_name`";
//$result_select_all_before = mysql_query($sql_select_all);
//$num_rows_before = mysql_num_rows($result_select_all_before);

// Insert Into
$sql="DELETE FROM `$tbl_name` WHERE ID = '$myID'";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

// Select * After
//$result_select_all_after=mysql_query($sql_select_all);
//$num_rows_after= mysql_num_rows($result_select_all_after);

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";
if ($rc == 1)	
{
	echo "Success, deleted ID " . "'$myID'";
}
else
{
	echo "Failure, not deleted ID " . "'$myID'";
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>
