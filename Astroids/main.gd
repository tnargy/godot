extends Node

signal SCORE_CHANGED
signal LEVEL_CHANGED
signal LIVES_CHANGED
signal GAME_OVER

var player_node

var score:
	get:
		return score
	set(value):
		score = value
		SCORE_CHANGED.emit(score)
		
var level:
	get:
		return level
	set(value):
		level = value
		LEVEL_CHANGED.emit(level)
		
var lives:
	get:
		return lives
	set(value):
		lives = value
		LIVES_CHANGED.emit(lives)


var num_astroids = 3
var astroid_spawn_range_min = 100
var astroid_spawn_range_max = 300

var player_scene = preload("res://player.tscn")
var astroid_big_scene = preload("res://astroid_big.tscn")

@onready var viewport_size = get_viewport().size
@export var astroid_container : Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	setup_new_game()
	

func cleanup():
	if player_node:
		player_node.queue_free()
		player_node = null
		
	for a in astroid_container.get_children():
		a.queue_free()
	

func setup_new_game():
	cleanup()
	score = 0
	level = 0
	lives = 3
	setup_new_level(num_astroids)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if num_astroids == 0:
		print("End of Game")
		get_tree().reload_current_scene()


func _on_player_death():
	lives -= 1
	if lives <= 0:
		game_over()
		return
	spawn_player()


func game_over():
	cleanup()
	GAME_OVER.emit()


func setup_new_level(num_ast):
	# TODO Make multiple levels
	level += 1
	spawn_player()
	
	for i in num_ast:
		spawn_astroid()


func spawn_player():
	if player_node:
		player_node.queue_free()
	
	player_node = player_scene.instantiate()
	player_node.position = viewport_size/2
	player_node.has_died.connect(_on_player_death)
	call_deferred("add_child",player_node)
	
	
func spawn_astroid():
	var a = astroid_big_scene.instantiate()
	a.position = viewport_size/2.0 + (
		Utility.RandomUnitVector2() * randf_range(astroid_spawn_range_min,astroid_spawn_range_max))
	astroid_container.call_deferred("add_child", a )

  
func add_to_score(n):
	# TODO create a score for each astroid
	pass

