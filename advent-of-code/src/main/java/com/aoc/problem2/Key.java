package com.aoc.problem2;

public class Key {

	private String name;
	private String up;
	private String down;
	private String left;
	private String right;
	
	public Key(String name, String up, String down, String left, String right){
		this.name = name;
		this.up = up;
		this.down = down;
		this.left = left;
		this.right = right;
	}

	public String getNumber() {
		return name;
	}

	public String getUp() {
		return up;
	}
	
	public String getDown() {
		return down;
	}
	
	public String getRight() {
		return right;
	}
	
	public String getLeft() {
		return left;
	}
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((name == null) ? 0 : name.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Key other = (Key) obj;
		if (name == null) {
			if (other.name != null)
				return false;
		} else if (!name.equals(other.name))
			return false;
		return true;
	}
	
}
