<?php
	require '../../db/db.php';

	$user_id=$_POST["userIDPost"];
	$friend_id=$_POST["friendIDPost"];

	// messagelist number, count 가져오기
	if(strcasecmp($user_id, $friend_id) >0)
	{
		$sql = "SELECT number, message_count FROM messagelist WHERE user1_id = '".$user_id."' and user2_id = '".$friend_id."'";
		$result = mysqli_query($conn, $sql);
		if(mysqli_num_rows($result)>0) //message_list에 존재(이전에 채팅한 기록이 있다.)
		{
			$row = mysqli_fetch_assoc($result);
			echo $row['number'].','.$row['message_count'].','; //대화 몇개 주고받았는지 확인
		}
		else  echo "fail";
	}
	else
	{
		$sql = "SELECT number, message_count FROM messagelist WHERE user1_id = '".$friend_id."' and user2_id = '".$user_id."'";
		$result = mysqli_query($conn, $sql);
		if(mysqli_num_rows($result)>0) //message_list에 존재(이전에 채팅한 기록이 있다.)
		{
			$row = mysqli_fetch_assoc($result);
			echo $row['number'].','.$row['message_count'].','; //대화 몇개 주고받았는지 확인
		}
		else  echo "fail";
	}
?>
