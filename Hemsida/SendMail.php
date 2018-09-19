<?php 

require 'PHPMailer-master/PHPMailerAutoload.php';

$number = 13;
$xml = '<xml>&#xD;<tag_abc>abc</tag_abc>&#xD;<tom_tag></tom_tag>&#xD;<tag_def>def</tag_def>&#xD;</xml>';

# Ersätter alla <- och >-tecken
$xml = str_replace("<", "&lt;", $xml);
$xml = str_replace(">", "&gt;", $xml);

$mail = new PHPMailer;

$mail->isSMTP(); 
$mail->Host = 'mailcluster.loopia.se'; 
$mail->SMTPAuth = true; 
$mail->Username = 'no-reply@sg-systemet.com'; // SMTP username
$mail->Password = 'kjw3eizd48'; // SMTP password
$mail->SMTPSecure = 'tls'; 
$mail->Port = 587;
$mail->CharSet = 'UTF-8';
$mail->ContentType = 'texe/xml';

#$mail->From = 'no-reply.skogensgodsling@yara.com';
$mail->From = 'no-reply@sg-systemet.com';
$mail->FromName = 'Skogens Gödsling';
#$mail->addAddress('karolina.erikers@yara.com');
$mail->addAddress('martin.thorbjornsson@sweco.se');

$mail->Subject = 'Testmejl från Skogens Gödsling, ' . $number;
#$mail->Body = 'HTML-body <b>Test fetstil!</b>';
$mail->Body = 'HTML: <pre>' . $xml . '</pre>';
$mail->AltBody = 'Vanlig "plain" text: ' . $xml;

$mail->isHTML(false);
$mail->AuthType = 'PLAIN';

if(!$mail->send()) {
 echo 'Kan inte skicka mejl. ' . $number;
 echo 'Felmeddelande: ' . $mail->ErrorInfo;
} else {
 echo 'Mejl är skickat. ' . $number;
}

?>