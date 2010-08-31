gci . *.sql -r | %{ 
	$name = $_.Name
	$fullname = $_.FullName
	$content = get-content $fullname
	
	$content | %{
		#use regex
		$r = [regex]"(?<db>\w+)\.\.(?<table>\w+)"
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