<?php
	require '../../db/db.php';

	$table_number = $_POST["tableNumPost"];
	$user_id=$_POST["userIdPost"];
	$friend_id = $_POST["friendIdPost"];
	$message_text=$_POST["textPost"];
	$message_count = $_POST["messageCountPost"];

	//messagerecord에 내용 추가
	$sql = "INSERT INTO messagerecord(list_number, sender_id, receiver_id, message_txt, message_order)VALUES('".$table_number."','".$user_id."','".$friend_id."','".$message_text."','".$message_count."')";
	$result = mysqli_query($conn,$sql);
	if($result){
		//messagelist의 count update
		$sql = "UPDATE messagelist SET message_count='".$message_count."' WHERE number=$table_number "; 
		$result = mysqli_query($conn,$sql);
		if($result){
			echo "success";
		}
		else echo "fail";
	}
	else echo "fail";
?>
