<?php 

require 'PHPMailer-master/PHPMailerAutoload.php';


# Hämtar data ifrån REQUEST
$order = $_REQUEST["ordernummer"];
$xml = $_REQUEST["xml"];
$text = $_REQUEST["text"];
$ton = $_REQUEST["ton"];
$foretag = $_REQUEST["foretag"];
$kontakt1 = $_REQUEST["kontakt1"];

# Ersätter alla å, ä, ö m.fl.
$text = str_replace("&aring", "å", $text);
$text = str_replace("&Aring", "Å", $text);
$text = str_replace("&auml", "ä", $text);
$text = str_replace("&Auml", "Ä", $text);
$text = str_replace("&ouml", "ö", $text);
$text = str_replace("&Ouml", "Ö", $text);
$text = str_replace("&uuml", "ü", $text);
$text = str_replace("&Uuml", "Ü", $text);
$text = str_replace("&ucirc", "û", $text);
$text = str_replace("&Ucirc", "Û", $text);
$text = str_replace("&egrave", "é", $text);
$text = str_replace("&Egrave", "É", $text);
$text = str_replace("&eacute", "è", $text);
$text = str_replace("&Eacute", "È", $text);
$text = str_replace("&amp", "&", $text);
$text = str_replace("&lt", "<", $text);
$text = str_replace("&gt", ">", $text);
$text = str_replace("&quot", '"', $text);
$text = str_replace("&#39", "'", $text);

$foretag = str_replace("&aring", "å", $foretag);
$foretag = str_replace("&Aring", "Å", $foretag);
$foretag = str_replace("&auml", "ä", $foretag);
$foretag = str_replace("&Auml", "Ä", $foretag);
$foretag = str_replace("&ouml", "ö", $foretag);
$foretag = str_replace("&Ouml", "Ö", $foretag);
$foretag = str_replace("&uuml", "ü", $foretag);
$foretag = str_replace("&Uuml", "Ü", $foretag);
$foretag = str_replace("&ucirc", "û", $foretag);
$foretag = str_replace("&Ucirc", "Û", $foretag);
$foretag = str_replace("&egrave", "é", $foretag);
$foretag = str_replace("&Egrave", "É", $foretag);
$foretag = str_replace("&eacute", "è", $foretag);
$foretag = str_replace("&Eacute", "È", $foretag);
$foretag = str_replace("&amp", "&", $foretag);
$foretag = str_replace("&lt", "<", $foretag);
$foretag = str_replace("&gt", ">", $foretag);
$foretag = str_replace("&quot", '"', $foretag);
$foretag = str_replace("&#39", "'", $foretag);

$kontakt1 = str_replace("&aring", "å", $kontakt1);
$kontakt1 = str_replace("&Aring", "Å", $kontakt1);
$kontakt1 = str_replace("&auml", "ä", $kontakt1);
$kontakt1 = str_replace("&Auml", "Ä", $kontakt1);
$kontakt1 = str_replace("&ouml", "ö", $kontakt1);
$kontakt1 = str_replace("&Ouml", "Ö", $kontakt1);
$kontakt1 = str_replace("&uuml", "ü", $kontakt1);
$kontakt1 = str_replace("&Uuml", "Ü", $kontakt1);
$kontakt1 = str_replace("&ucirc", "û", $kontakt1);
$kontakt1 = str_replace("&Ucirc", "Û", $kontakt1);
$kontakt1 = str_replace("&egrave", "é", $kontakt1);
$kontakt1 = str_replace("&Egrave", "É", $kontakt1);
$kontakt1 = str_replace("&eacute", "è", $kontakt1);
$kontakt1 = str_replace("&Eacute", "È", $kontakt1);
$kontakt1 = str_replace("&amp", "&", $kontakt1);
$kontakt1 = str_replace("&lt", "<", $kontakt1);
$kontakt1 = str_replace("&gt", ">", $kontakt1);
$kontakt1 = str_replace("&quot", '"', $kontakt1);
$kontakt1 = str_replace("&#39", "'", $kontakt1);

 
# Mejl till kunden
$mailCustomer = new PHPMailer;
$mailCustomer->isSMTP(); 
$mailCustomer->Host = 'mailcluster.loopia.se'; 
$mailCustomer->SMTPAuth = true; 
$mailCustomer->Username = 'no-reply@sg-systemet.com';
$mailCustomer->Password = 'kjw3eizd48';
$mailCustomer->SMTPSecure = 'tls'; 
$mailCustomer->Port = 587;
$mailCustomer->CharSet = 'UTF-8';

$mailCustomer->From = 'no-reply@sg-systemet.com';
$mailCustomer->FromName = 'Beställning av SG-systemet';
$mailCustomer->addAddress('' . $kontakt1);

$textHTML = str_replace("\n", "<br>", $text);

$mailCustomer->Subject = 'Beställning hos Skogens Gödsling - Ordernummer ' . $order;
$mailCustomer->Body = 'Tack för din beställning av skogsgödsling med SG-systemet avseende ' . $ton . ' ton Skog-CAN.<br><br>Din beställning har skickats till Skogens Gödslings AB och ingår i planeringsunderlaget för gödslingssäsongen. Du har väl även skickat kartfiler?<br>När säsongens alla beställningar och kartfiler inkommit och spridningsplanen är gjord, kan slutlig bekräftelse av beställningen göras.<br><br>Nedan kan du se din beställning.<br><br>Beställning av SG-gödsling:<br><br>' . $textHTML . '<br><br>Med vänliga hälsningar<br>Skogens Gödslings AB<br>skogensgodsling@yara.com<br>0418-765 00';
$mailCustomer->AltBody = 'Tack för din beställning av skogsgödsling med SG-systemet avseende ' . $ton . ' ton Skog-CAN.\n\nDin beställning har skickats till Skogens Gödslings AB och ingår i planeringsunderlaget för gödslingssäsongen. Du har väl även skickat kartfiler?\nNär säsongens alla beställningar och kartfiler inkommit och spridningsplanen är gjord, kan slutlig bekräftelse av beställningen göras.\n\nNedan kan du se din beställning.\n\nBeställning av SG-gödsling:\n\n' . $text . '\n\nMed vänliga hälsningar\nSkogens Gödslings AB\nskogensgodsling@yara.com\n0418-765 00';

if(!$mailCustomer->send()) {
 echo 'Kan inte skicka mejl.';
 echo 'Felmeddelande: ' . $mailCustomer->ErrorInfo;
} else {
 echo 'Mejl är skickat: ' . 'Tack för din beställning av skogsgödsling med SG-systemet avseende ' . $ton . ' ton Skog-CAN.<br><br>Din beställning har skickats till Skogens Gödslings AB och ingår i planeringsunderlaget för gödslingssäsongen. Du har väl även skickat kartfiler?<br>När säsongens alla beställningar och kartfiler inkommit och spridningsplanen är gjord, kan slutlig bekräftelse av beställningen göras.<br><br>Nedan kan du se din beställning.<br><br>Beställning av SG-gödsling:<br><br>' . $textHTML . '<br><br>Med vänliga hälsningar<br>Skogens Gödslings AB<br>skogensgodsling@yara.com<br>0418-765 00';
}

# Mejl till Skogens Gödsling
$mailSG = new PHPMailer;
$mailSG->isSMTP(); 
$mailSG->Host = 'mailcluster.loopia.se'; 
$mailSG->SMTPAuth = true; 
$mailSG->Username = 'no-reply@sg-systemet.com';
$mailSG->Password = 'kjw3eizd48';
$mailSG->SMTPSecure = 'tls'; 
$mailSG->Port = 587;
$mailSG->CharSet = 'UTF-8';

$mailSG->From = 'no-reply@sg-systemet.com';
$mailSG->FromName = 'Beställning av SG-systemet';
$mailSG->addAddress('skogensgodsling@yara.com');

# Ersätter alla nyradstecken, <- och >-tecken
$xmlHTML = str_replace("\n", "&#xD;", $xml);
$xmlHTML = str_replace("\n", "&#009;", $xmlHTML);
$xmlHTML = str_replace("<", "&lt;", $xmlHTML);
$xmlHTML = str_replace(">", "&gt;", $xmlHTML);
$xmlHTML = '<pre>' . $xmlHTML . '</<pre>';

$mailSG->Subject = 'Ordernummer: ' . $order . '  |  ' .  $foretag . ' - ' . $kontakt1;
$mailSG->Body = 'Beställning av skogsgödsling med SG-systemet avseende ' . $ton . ' ton Skog-CAN.<br><br>' . $xmlHTML;
$mailSG->AltBody = 'Beställning av skogsgödsling med SG-systemet avseende ' . $ton . ' ton Skog-CAN.\n\n' . $xml;

if(!$mailSG->send()) {
 echo 'Kan inte skicka mejl.';
 echo 'Felmeddelande: ' . $mailSG->ErrorInfo;
} else {
 echo 'Mejl är skickat: ' . 'Beställning av skogsgödsling med SG-systemet avseende ' . $ton . ' ton Skog-CAN.<br><br>' . $xmlHTML;
}

?>