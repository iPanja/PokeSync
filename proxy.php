<?php
//ALL VARIABLES SPECIFIED
$required = ["code", "client_id", "redirect_uri"];
foreach($required as $r){
  if(!isset($_GET[$r]) || empty($_GET[$r])){
    echo("Not all required values have been specified!");
    exit;
  }
}
//SANITIZE
$code = filter_var($_GET['code'], FILTER_SANITIZE_STRING);
$client_id = filter_var($_GET['code'], FILTER_SANITIZE_STRING);
$redirect_uri = filter_var($_GET['code'], FILTER_SANITIZE_STRING);
//REDIRECT
$client_secret = "vtkge5ui3osl1oc";
$url = "https://api.dropboxapi.com/oauth2/token";
$data = array(
  "client_id" => urlencode($client_id),
  "client_secret" => urlencode($client_secret),
  "grant_type" => urlencode("authorization_code"),
  "redirect_uri" => urlencode($redirect_uri),
  "code" => urlencode($code)
);
$options = array(
  'http' => array(
        'header'  => ("Authorization: Bearer " . $code),
        'method'  => 'POST',
        'content' => http_build_query($data)
    )
);
$context = stream_context_create($options);
$result = file_get_contents($url, false, $context);
echo(json_encode($result));
exit;
///

?>
