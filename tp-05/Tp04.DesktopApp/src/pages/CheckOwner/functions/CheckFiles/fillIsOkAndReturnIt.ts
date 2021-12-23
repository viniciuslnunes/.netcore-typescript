import IFile from '../../interfaces/IFile'

interface IFillIsOkAndReturnItParams {
  /** The files you are checking */
  files: IFile[];
}

/**
 * This function uses `errors` array to fill `isOK` property,
 * informing if file contains errors or its ok
 *
 * @returns The files with `isOk` property filled
 */
export function fillIsOkAndReturnIt ({
  files
}: IFillIsOkAndReturnItParams): IFile[] {
  return files.map((file) => {
    file.isOk = !file.errors?.length

    return file
  })
}
