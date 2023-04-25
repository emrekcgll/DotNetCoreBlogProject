<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta name="description" content="HOMEALARMS - Alarms and security systems site template">
	<meta name="author" content="Ansonika">
	<title>HOMEALARMS - Alarms and security systems site template</title>

    <!-- Favicons-->
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="apple-touch-icon" type="image/x-icon" href="img/apple-touch-icon-57x57-precomposed.png">
    <link rel="apple-touch-icon" type="image/x-icon" sizes="72x72" href="img/apple-touch-icon-72x72-precomposed.png">
    <link rel="apple-touch-icon" type="image/x-icon" sizes="114x114" href="img/apple-touch-icon-114x114-precomposed.png">
    <link rel="apple-touch-icon" type="image/x-icon" sizes="144x144" href="img/apple-touch-icon-144x144-precomposed.png">

    <!-- GOOGLE WEB FONT -->
	<link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet">

    <!-- BASE CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
	<link href="css/style.css" rel="stylesheet">
	<link href="css/menu.css" rel="stylesheet">
	<link href="css/responsive.css" rel="stylesheet">
	<link href="css/elegant_font/elegant_font.min.css" rel="stylesheet">
	<link href="css/fontello/css/fontello.min.css" rel="stylesheet">
    
	<script type="text/javascript">
    function delayedRedirect(){
        window.location = "index.html"
    }
    </script>

</head>
<body id="confirmation" onLoad="setTimeout('delayedRedirect()', 10000)">
<?php
						$mail = $_POST['email_quote'];

						/*$subject = "".$_POST['subject'];*/
						$to = "info@domain.com";
						$subject = "Quotation request from HOMEALARMS";
						$headers = "From: HOMEALARMS web site <noreply@yourdomain.com>";
						$message = "\nDETAILS"; 
						$message .= "\nWhere do you need to install the Alarm? " . $_POST['location'];
						$message .= "\nDo you have the armored door. If yes, how many? " . $_POST['armored_door'];
						$message .= "\nHow many windows do you want to protect? " . $_POST['windows'];
						$message .= "\nHow many areas do you want to protect? " . $_POST['zones'];
						$message .= "\nDoes the house have secondary accesses?\n" ;
						foreach($_POST['accesses'] as $value) 
							{ 
							$message .=   "- " .  trim(stripslashes($value)) . "\n"; 
							};
						$message .= "\nWhat is your budget? " . $_POST['budget'];
						$message .= "\n\nUSER DETAILS"; 
						$message .= "\nName: " . $_POST['firstname_quote'];
						$message .= "\nLast Name: " . $_POST['lastname_quote'];
						$message .= "\nEmail: " . $_POST['email_quote'];
						$message .= "\nPhone number: " . $_POST['phone_quote'];
						$message .= "\nAdditional info: " . $_POST['message_general'];
						$message .= "\nTerms and conditions accepted: " . $_POST['terms'];
						
						//Receive Variable
						$sentOk = mail($to,$subject,$message,$headers);
						
						//Confirmation page
						$user = "$mail";
						$usersubject = "Thank You";
						$userheaders = "From: info@homealarms.com\n";
						/*$usermessage = "Thank you for your time. Your request is successfully submitted.\n"; WITH OUT SUMMARY*/
						//Confirmation page WITH  SUMMARY
						$usermessage = "Thank you for your time. Your request is successfully submitted.\n\nSUMMARY\n$message"; 
						mail($user,$usersubject,$usermessage,$userheaders);
	
?>

<!-- END SEND MAIL SCRIPT -->   
<div id="success">
	<div class="icon icon--order-success svg">
		<svg xmlns="http://www.w3.org/2000/svg" width="72px" height="72px">
                <g fill="none" stroke="#8EC343" stroke-width="2">
                  <circle cx="36" cy="36" r="35" style="stroke-dasharray:240px, 240px; stroke-dashoffset: 480px;"></circle>
                  <path d="M17.417,37.778l9.93,9.909l25.444-25.393" style="stroke-dasharray:50px, 50px; stroke-dashoffset: 0px;"></path>
                </g>
              </svg>
	</div>
	<h4><span>Thank you!</span>Quotation request sent.</h4>
	<small>You will be redirect back in 10 seconds.</small>
</div>
</body>
</html>