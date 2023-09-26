<?php
	$servername = "localhost";
	$server_username ="root";
	$server_password = "0000";
	$dbname = "metaverse";
	
	//db컬럼
	$id=$_POST["idPost"];
	$password = $_POST["passwordPost"];
	$nickname = $_POST["usernamePost"];
	$email = $_POST["emailPost"];
	$gender = $_POST["genderPost"];


	//db연결
	$conn = new mysqli($servername, $server_username, $server_password, $dbname);
	
	if(!$conn)
	{
	  die("Connection Failed.". mysqli_connect_error());
	}
	$sql = "INSERT INTO userinfo (id,pw,nickname,email,gender) VALUES ('".$id."','".$password."','".$nickname."','".$email."','".$gender."')";
	$result = mysqli_query($conn, $sql);
	
	if($result==1)
	{
	  echo "저장성공";
	}else{
	  echo "저장실패";
	}
?>
