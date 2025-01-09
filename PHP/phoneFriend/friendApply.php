<?php
	require '../../db/db.php';

	$to_user_id=$_POST["userIdPost"];
	$from_user_id = $_POST["fromUserIdPost"];

	$sql = "INSERT INTO applyfriend (to_user_id,from_user_id) VALUES ('".$to_user_id."','".$from_user_id."')";
	$result = mysqli_query($conn, $sql);
	
	if($result==1)
	{
	  echo "저장성공";
	}else{
	  echo "저장실패";
	}
?>
