import React, { useCallback, useRef, useState } from 'react'
import { Link } from 'react-router-dom'
import { toast } from 'react-toastify'
import { shell } from 'electron'
import {
  DiCodeBadge,
  MdRefresh,
  BiError,
  BiCheckDouble,
  AiOutlineTool,
  FiSettings
} from 'react-icons/all'

import { useSettings } from '../../hooks/settings'
import { routesMap } from '../../config/routes-map'

import Button from '../../components/Button'
import NavBar from '../../components/NavBar'
import FileInput from '../../components/FileInput'

import IFile from './interfaces/IFile'

import { fixFiles } from './functions/FixFiles'
import checkFiles from './functions/CheckFiles'

import * as SC from './styles'

const CheckOwner: React.FC = () => {
  const { switchTermPairs, defaultOwner, packagePrefix } = useSettings()
  const [files, setFiles] = useState<IFile[]>([])
  const inputDirectoryRef = useRef<HTMLInputElement>(null)

  const handleOpenFile = useCallback((file: IFile) => {
    shell.openItem(file.path)
  }, [])

  const handleFixFiles = useCallback(() => {
    const filesToFix = files.filter((file) => !file.isOk)

    fixFiles(filesToFix, defaultOwner, packagePrefix, switchTermPairs).then(
      () => {
        setFiles([])
        toast.success('Arquivos corrigidos.')
      }
    )
  }, [files, switchTermPairs, defaultOwner, packagePrefix])

  const handleRecheck = useCallback(() => {
    const terms = switchTermPairs.map(({ oldTerm }) => oldTerm)

    checkFiles(files, terms, packagePrefix).then((checkedFiles) =>
      setFiles(checkedFiles)
    )
  }, [files, switchTermPairs, packagePrefix])

  const handleDirectoryChange = useCallback(() => {
    if (!inputDirectoryRef.current) return

    const directoryFiles = Array.from(
      inputDirectoryRef.current.files || []
    ) as IFile[]

    const SQLFiles = directoryFiles.filter(
      (file) => file.type === 'application/sql'
    )

    const terms = switchTermPairs.map(({ oldTerm }) => oldTerm)

    checkFiles(SQLFiles, terms, packagePrefix).then((checkedFiles) => {
      if (inputDirectoryRef?.current?.value) {
        inputDirectoryRef.current.value = ''
      }

      setFiles(checkedFiles)
    })
  }, [inputDirectoryRef, switchTermPairs, packagePrefix])

  return (
    <SC.Container>
      <NavBar>
        <Link to={routesMap.Settings.path}>
          <FiSettings size={24} />
        </Link>
      </NavBar>

      <SC.MainContent>
        <SC.FlexContainer>
          <FileInput
            type="directory"
            name="input-directory"
            ref={inputDirectoryRef}
            onChange={handleDirectoryChange}
          />

          {!!files.length && (
            <Button icon={MdRefresh} onClick={handleRecheck}>
              Checar novamente
            </Button>
          )}
        </SC.FlexContainer>

        <SC.FileList>
          <thead>
            <tr>
              <th>Nome</th>
              <th className="icon">Abrir</th>
              <th className="icon">Situação</th>
            </tr>
          </thead>
          <tbody>
            {files.map((file) => (
              <tr key={file.name}>
                <td>{file.name}</td>

                <td className="icon">
                  <button type="button" onClick={() => handleOpenFile(file)}>
                    <DiCodeBadge size={24} />
                  </button>
                </td>

                <td className="icon">
                  {file.isOk ? (
                    <BiCheckDouble color="#5beb5b" size={24} />
                  ) : (
                    <BiError color="#e85b51" size={24} />
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </SC.FileList>

        {!!files.some((file) => !file.isOk) && (
          <Button color="#5beb5b" icon={AiOutlineTool} onClick={handleFixFiles}>
            Corrigir todos
          </Button>
        )}
      </SC.MainContent>
    </SC.Container>
  )
}

export default CheckOwner
