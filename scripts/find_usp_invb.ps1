gci . *.vb -r | %{ 
	$name = $_.Name
	$fullname = $_.FullName
	$content = get-content $fullname
	
	$content | %{
		$r = [regex]"usp[_A-Za-z]+"
		$o = $r.Match($_)
		if($o.Success)
		{
			$o | %{
				$v = $_.Value
				Write-Output "$name -> $v"
			}
		}
	}
}