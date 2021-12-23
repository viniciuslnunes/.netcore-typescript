import IFile from '../interfaces/IFile'

interface IFillEditableContentParams {
  files: IFile[];
}

/**
 * This function populate `editableContent` property of type `IFile`.
 *
 * Since it can only be accessed through an async method,
 * this property will give the possibility to access,
 * and modify, its content synchronously
 *
 *  @param files The files will you want make editable
 */
export async function fillEditableContent ({
  files
}: IFillEditableContentParams): Promise<void> {
  await Promise.all(
    files.map(async (file: IFile) => {
      file.editableContent = await file.text()
    })
  )
}
