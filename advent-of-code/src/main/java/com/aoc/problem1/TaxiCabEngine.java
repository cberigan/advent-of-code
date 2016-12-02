package com.aoc.problem1;

import java.awt.Point;
import java.util.HashSet;

public class TaxiCabEngine {
	
	Point position;
	Point hq;
	boolean newHQFound = false;
	
	HashSet<Point> path;
	
	Direction direction = Direction.North;
	
	public TaxiCabEngine(){
		path = new HashSet<Point>();
		position = new Point(0,0);
		hq = position.getLocation(); //supposed location of hq
		path.add(position.getLocation());
	}
	
	public void SendMove(Move m){
		//position car
		SetNewDirection(m.getTurn());
		MoveInDirection(m.getSteps());
	}
	
	private void MoveOneUnit(){
		switch(direction){
			case North: position.translate(0, 1);
			break;
			case South: position.translate(0, -1);
			break;
			case East: position.translate(1, 0);
			break;
			case West: position.translate(-1, 0);
			break;
		}
		//check if position is seen before
		if(!newHQFound && path.contains(position.getLocation()) ){
			hq = position.getLocation();
			newHQFound = true;
		} else if(!newHQFound){
			path.add(position.getLocation());
		}
	}

	private void MoveInDirection(int steps) {
		for(int i = 1; i <= steps;i++){
			MoveOneUnit();
		}
	}

	private void SetNewDirection(Turn turn) {
		switch(direction){
			case North:
				if(turn == Turn.Left){
					direction = Direction.West;
				}else{
					direction = Direction.East;
				}
			break;
			case South:
				if(turn == Turn.Left){
					direction = Direction.East;
				}else{
					direction = Direction.West;
				}
			break;
			case East:
				if(turn == Turn.Left){
					direction = Direction.North;
				}else{
					direction = Direction.South;
				}
			break;
			case West:
				if(turn == Turn.Left){
					direction = Direction.South;
				}else{
					direction = Direction.North;
				}
			break;
		}
	}

	public int GetDistanceFromHQ() {
		return (int) (Math.abs(0 - hq.getX()) + Math.abs(0 - hq.getY()));
	}

}
