extends Node

var player_node

var num_astroids = 3
var astroid_spawn_range_min = 100
var astroid_spawn_range_max = 300

var player_scene = preload("res://player.tscn")
var astroid_big_scene = preload("res://astroid_big.tscn")

@onready var viewport_size = get_viewport().size

# Called when the node enters the scene tree for the first time.
func _ready():
	setup_new_level(num_astroids)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_player_death():
	spawn_player()


func setup_new_level(num_ast):
	spawn_player()
	
	for i in num_astroids:
		spawn_astroid()


func spawn_player():
	if player_node:
		player_node.queue_free()
	
	player_node = player_scene.instantiate()
	player_node.position = viewport_size/2
	player_node.has_died.connect(_on_player_death)
	add_child(player_node)
	
	
func spawn_astroid():
	var a = astroid_big_scene.instantiate()
	a.position = viewport_size/2.0 + (
		Utility.RandomUnitVector2() * randf_range(astroid_spawn_range_min,astroid_spawn_range_max))
	add_child(a)


