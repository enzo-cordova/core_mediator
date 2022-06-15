Feature: SampleCreate

@SampleCreate
Scenario: Create Sample
	Given Un sample con los datos:
	| Key  | Value    |
	| Name | sample |
	| SubSampleId | 1       |
	When Se envIa sample al API Rest
	Then Se crea sample con los datos:
	| Key  | Value    |
	| Name | sample |
	| SubSampleId | 1       |
	And Se borra sample
	And La respuesta HTTP sample es OK

	