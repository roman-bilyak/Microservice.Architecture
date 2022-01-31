param (
	[string] $tag = '',
	[switch] $build = $true,
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048
)

.\_run.ps1 -tag $tag -build:$build -cpus $cpus -memory $memory