param (
	[string] $tag = 'v1.0',
	[switch] $build = $true,
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048,
	[Int32][ValidateRange(1, 10)] $nodes = 5
)

.\_run.ps1 -registry 'localhost:5000' -tag $tag -build:$build -cpus $cpus -memory $memory -nodes $nodes