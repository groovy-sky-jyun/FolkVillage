<?php
	require '../../db/db.php';

	$id=$_POST["idPost"];
	$password = $_POST["passwordPost"];
	$nickname = $_POST["usernamePost"];
	$email = $_POST["emailPost"];
	$gender = $_POST["genderPost"];

	$sql = "INSERT INTO userinfo (id,pw,nickname,email,gender) VALUES ('".$id."','".$password."','".$nickname."','".$email."','".$gender."')";
	$result = mysqli_query($conn, $sql);
	
	if($result==1)
	{
	  echo "저장성공";
	}else{
	  echo "저장실패";
	}
?>
