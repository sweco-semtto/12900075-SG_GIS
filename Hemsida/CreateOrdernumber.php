<?php 
	$host="mysql410.loopia.se"; // Host name 
	$username="UNLYdfR9@s142821"; // Mysql username 
	$password="2ykgB03hnx"; // Mysql password 
	$db_name="sg_systemet_com"; // Database name 
	$tbl_name="SG_Ordernummer"; // Table name 

	// Connect to server and select database.
	mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
	mysql_select_db("$db_name")or die("cannot select DB");	
	
	// Data sent from form javascript
	$Year = $_POST['Year'];
	$Timestamp = $_POST['Timestamp']; 

	// Tar fram vilket som är högsta id-numret och lägger till ett. 
	$sql="SELECT MAX(Ordernummer) FROM `$tbl_name` where Ar='" . $Year . "'";
	$result=mysql_query($sql);
	$Ordernummer=0;
	
	// Har bara ett ordernummer och tar fram det. 
	while($row = mysql_fetch_array($result))
	{
		$Ordernummer = intval($row["MAX(Ordernummer)"]) + 1;
	}
	
	// Skapar ett nytt ordernummer
	$sql="INSERT INTO `$tbl_name` (Ordernummer, Ar, Tid) VALUES ($Ordernummer, '$Year', '$Timestamp')";	
	$result=mysql_query($sql);
	$rc = mysql_affected_rows();
	
	// Kontrollerar att en rad skapades för att veta att ordernumret finns i databasen. 
	if ($rc == 1)
	{
		echo "".$Ordernummer;
	}
	else
	{
		echo "0";
	}
?>