import fs from 'fs'
import { promisify } from 'util'

import IFile from '../../interfaces/IFile'

interface ISaveFixedFilesParams {
  /** The files will be saved */
  files: IFile[];
}

/**
 * This function updates physically the files with its corrections stored
 * in `editableContent` property of type `IFile`
 */
export async function saveFixedFiles ({
  files
}: ISaveFixedFilesParams): Promise<void> {
  const promisedWriteFile = promisify(fs.writeFile)

  const promisedLoops = files.map(async (file: IFile) => {
    try {
      await promisedWriteFile(file.path, file.editableContent, {
        encoding: 'utf-8'
      })
    } catch (error) {
      alert('An error ocurred updating the file' + error.message)
      console.error(error)
    }
  })

  await Promise.all(promisedLoops)
}
