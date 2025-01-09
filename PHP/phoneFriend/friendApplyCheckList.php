<?php
	require '../../db/db.php';
	
	$user_id=$_POST["idPost"];
	
	$sql = "SELECT to_user_id FROM applyfriend WHERE from_user_id = '".$user_id."' and are_we_friend = 0";
	$result = mysqli_query($conn, $sql);

	//나한테 온 친구 신청이 있다는 뜻
	if(mysqli_num_rows($result)>0)
	{
		//신청개수만큼 반복
		while($row = mysqli_fetch_assoc($result)){
			// 신청자의 닉네임, 성별 가져오기기
			getUserinfo($row['to_user_id']);
		};
	} 
	else
		echo "null";
	
	function getUserinfo($friend_id){
		global $conn;
		$sql = "SELECT nickname, gender FROM userinfo WHERE id='".$friend_id."'";
		$result = mysqli_query($conn, $sql);
		if(mysqli_num_rows($result)>0){
			$row = mysqli_fetch_assoc($result);
			echo $row['nickname'].','.$row['gender'].',';
		}
		else echo "fail";
	}
?>
