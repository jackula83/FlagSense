use FlagServiceDB
go

-- seed flags

insert into ..Flag (
	SegmentId,
	Name,
	Description,
	Alias,
	IsEnabled,
	DefaultServeValue,
	Uuid,
	DeleteFlag,
	CreatedAt,
	UpdatedAt,
	CreatedBy,
	UpdatedBy
) VALUES (
	NULL,
	'test-flag-1',
	'Lorem ipsum dolor sit, amet consectetur adipisicing elit. Iusto provident sapiente ab libero culpa id, voluptate eos totam dignissimos sequi, molestias, illum aliquid ipsa consequatur.',
	'test-flag-alias-1',
	0,
	'{"State":true}',
	NEWID(),
	0,
	GETDATE(),
	GETDATE(),
	'script',
	'script'
)

insert into ..Flag (
	SegmentId,
	Name,
	Description,
	Alias,
	IsEnabled,
	DefaultServeValue,
	Uuid,
	DeleteFlag,
	CreatedAt,
	UpdatedAt,
	CreatedBy,
	UpdatedBy
) VALUES (
	NULL,
	'test-flag-2',
	'Lorem ipsum dolor sit amet consectetur adipisicing elit. Molestiae hic tempora omnis numquam, odit dicta eligendi? Recusandae quibusdam pariatur quos esse eum voluptatum facilis ipsa eaque maiores sequi iure, debitis obcaecati officia est, praesentium reiciendis assumenda vero tempore accusamus inventore. Eligendi quidem perspiciatis numquam dolores sed consequatur nemo ducimus quo architecto iure? Odit porro, quae exercitationem suscipit, corrupti quo sunt sint quisquam delectus optio pariatur? Autem libero, vero facilis maiores, nesciunt amet voluptas sunt, quam architecto veritatis ipsa magnam inventore aut officia saepe. Minus, officiis et. Distinctio reiciendis perferendis numquam maiores ea aut vitae, voluptates similique, dolorum recusandae vel at?',
	'test-flag-alias-2',
	0,
	'{"State":true}',
	NEWID(),
	0,
	GETDATE(),
	GETDATE(),
	'script',
	'script'
)

insert into ..Flag (
	SegmentId,
	Name,
	Description,
	Alias,
	IsEnabled,
	DefaultServeValue,
	Uuid,
	DeleteFlag,
	CreatedAt,
	UpdatedAt,
	CreatedBy,
	UpdatedBy
) VALUES (
	NULL,
	'test-flag-3',
	'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis nulla voluptatem vitae tenetur, assumenda quibusdam nobis esse cupiditate temporibus pariatur fuga, laborum beatae hic ipsa?',
	'test-flag-alias-3',
	0,
	'{"State":true}',
	NEWID(),
	0,
	GETDATE(),
	GETDATE(),
	'script',
	'script'
)

select * from ..Flag