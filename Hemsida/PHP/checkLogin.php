<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Entreprenor"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($mysqli->connect_errno) {
    printf("Anslutningsfel: %s\n", $mysqli->connect_error);
    exit();
}

// username and password sent from form 
$myusername=$_POST['username']; 
$mypassword=$_POST['password']; 

// To protect MySQL injection (more detail about MySQL injection)
$myusername = stripslashes($myusername);
$mypassword = stripslashes($mypassword);
$myusername = $con->real_escape_string($myusername);
$mypassword = $con->real_escape_string($mypassword);

$sql="SELECT * FROM `$tbl_name` WHERE `Anvandarnamn`='" . $myusername . "' and `Losenord`='" . $mypassword . "'";
$result = $con->query($sql);

$row = $result->fetch_array(MYSQLI_ASSOC);

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";
echo "<ID>" . $row["ID"] . "</ID>";
echo "\n";
while($row = $result->fetch_assoc())
{
	echo "<ID>" . $row["ID"] . "</ID>";	
	echo "\n";
}
echo "</Data>";
echo "\n";
echo "</MessageXML>";

$result->free();
$con->close();
?>