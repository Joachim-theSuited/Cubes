bl_info = {
    "name": "Export Helper",
    "category": "Object",
    "author": "Rafael",
    "version": (1, 0),
    }

import bpy
import mathutils

from mathutils import Vector, Matrix

class ExportHelperOp(bpy.types.Operator):
    bl_idname = "mesh.nice_export"
    bl_label = "Prepare for Export"
    bl_options = {"REGISTER", "UNDO"}

    def execute(self, context):
        for obj in context.selected_objects:
            if obj.type != 'MESH':
                continue # just ignore for now

            # move origin to bottom
            bb_center = sum( ( Vector(corner) for corner in obj.bound_box ) , Vector() ) / 8
            obj.data.transform( Matrix.Translation( (0, 0, -bb_center.z + obj.dimensions.z/( 2 * obj.scale.z )) ) )
            obj.location.z = 0

            # remove scaling
            scaleX = Matrix.Scale(obj.scale.x, 4, (1.0, 0.0, 0.0))
            scaleY = Matrix.Scale(obj.scale.y, 4, (0.0, 1.0, 0.0))
            scaleZ = Matrix.Scale(obj.scale.z, 4, (0.0, 0.0, 1.0))
            obj.data.transform( scaleX * scaleY * scaleZ )
            obj.scale = (1, 1, 1)

            obj.data.update()

        return {'FINISHED'}


#############################################
# un-/register formalities
#############################################

def register():
    bpy.utils.register_class(ExportHelperOp)

def unregister():
    bpy.utils.unregister_class(ExportHelperOp)

if __name__ == "__main__":
    register()
