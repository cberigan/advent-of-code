package com.aoc.problem1;

public class Move {
	
	private Turn turn;
	private int steps;
	
	public Move(Turn t, int s){
		this.turn = t;
		this.steps = s;
	}
	
	public Turn getTurn(){
		return turn;
	}
	
	public int getSteps(){
		return steps;
	}
}
