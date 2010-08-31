$databases = @{}
gci . *.sql -r | %{ 
	$name = $_.Name
	$fullname = $_.FullName
	$content = get-content $fullname
	
	$content | %{
		$r = [regex]"(?<db>\w+)\.dbo"
		$o = $r.Match($_)
		if($o.Success)
		{
			$o | %{
				$v = $_.Groups['db']
				if(!$databases.ContainsKey($v))
				{
					$databases.Add($v,0)
				}
				$databases[$v] = $databases[$v] + 1
				Write-Output "$name -> $v"
			}
		}
	}
}