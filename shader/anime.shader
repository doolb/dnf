shader_type canvas_item;

uniform int graphicEffect = 0;

void fragment(){
	vec4 col = texture(TEXTURE, UV); //read from texture
	if(graphicEffect == 1){
		float cmax = max(col.r, max(col.g, col.b));
		float sub = 1.0 - cmax;
		col.a = min(col.a, cmax);
		col.r += sub;
		col.g += sub;
		col.b += sub;
	}
	COLOR = col;
}