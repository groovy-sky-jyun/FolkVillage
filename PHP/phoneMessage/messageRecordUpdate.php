
<?php
	require '../../db/db.php';

	$number=$_POST["numberPost"];
	$user_id=$_POST["userIDPost"];

	#대화창에서 기본으로 세팅된 메시지 이후로 상대방이 나한테 보낸 경우 실시간으로 메시지 업데이트 해줌
	$sql = "SELECT * FROM messagerecord WHERE read_check = 0 AND list_number = '".$number."'AND receiver_id='".$user_id."'";
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows($result)>0){
		while($row = mysqli_fetch_assoc($result)){
			$sql = "UPDATE messagerecord SET read_check=1 WHERE message_order = '".$row['message_order']."' AND list_number = '".$number."'";
			$result = mysqli_query($conn, $sql);	
			
			if($result){
				echo "fail";
			}
			echo $row['sender_id'].",".$row['message_txt'].",".$row['message_time'].",".$row['message_order'].",".$row['read_check'].'/';
		}
	}
	else echo "fail";

	
?>
