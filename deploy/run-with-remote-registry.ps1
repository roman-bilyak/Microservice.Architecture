param (
	[string] $tag = 'v1.0',
	[switch] $build = $false,
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048,
	[Int32][ValidateRange(1, 10)] $nodes = 1
)

.\run.ps1 -registry 'romanbilyak' -tag $tag -build:$build -cpus $cpus -memory $memory -nodes $nodes