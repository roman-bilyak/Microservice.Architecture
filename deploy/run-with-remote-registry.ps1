param (
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048,
	[Int32][ValidateRange(1, 10)] $nodes = 1,
	[string] $tag = '',
	[switch] $build = $false
)

.\run.ps1 -registry 'romanbilyak' -tag $tag -build:$build -cpus $cpus -memory $memory -nodes $nodes