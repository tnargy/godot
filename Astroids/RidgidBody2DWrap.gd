extends RigidBody2D

class_name RigidBody2DWrap

@onready var viewport_size = get_viewport().size
@onready var sprite_size = $Sprite2D.get_rect().size

func _integrate_forces(state):
	wrap_to_viewport(state)

func wrap_to_viewport(state):
	# Left
	if state.transform.origin.x + sprite_size.x/2 < 0:
		state.transform.origin.x = viewport_size.x
	# Right
	if state.transform.origin.x - sprite_size.x/2> viewport_size.x:
		state.transform.origin.x = -sprite_size.x/2
	# Up
	if state.transform.origin.y + sprite_size.y/2 < 0:
		state.transform.origin.y = viewport_size.y
	# Down
	if state.transform.origin.y - sprite_size.y/2 > viewport_size.y:
		state.transform.origin.y = -sprite_size.y/2
	pass
